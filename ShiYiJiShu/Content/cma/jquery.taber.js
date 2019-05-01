/**
 * [ xTaber 鍒囨崲 ]
 * @param  {[type]} $ [description]
 * @return {[type]}   [description]
 */
(function($){
    $.fn.extend({
        xTaber: function(opt){
            var def = $.extend({
                /* @tab 瑙﹀彂浜嬩欢鏍囩 [true|false|obj]
                 * true 鑷姩鐢熸垚甯︽暟瀛楃殑鏍囩
                 * false 涓嶆樉绀簍ab
                 * obj 鑷畾涔塼ab
                 */
                tab: true, //榛樿涓鸿嚜鍔ㄧ敓鎴�
                content:$('#xtaberWrap'),
                prev: null, //涓婁竴涓寜閽�
                next: null, //涓嬩竴涓寜閽�
                /* @style 婊氬姩鐨勬牱寮� [opacity|left|top|none]
                 * opacity 娣″嚭娣″叆
                 * left 鍚戝乏
                 * top 鍚戜笂
                 * none 鏃犳晥鏋�
                 */
                style: 'opacity', //榛樿涓簅pacity
                activeClass: 'current', //褰撳墠鏍峰紡
                delay: 100, //鎿嶄綔寤舵椂
                speed: 300, //绉诲姩閫熷害
                timeout: 3000, //闂存瓏鏃堕棿
                auto: false, //鏄惁鑷姩婊氬姩
                setup: 1,//姣忔婊氬姩澶氬皯涓�
                defaultShow: 1, //榛樿鏄剧ず绗琻涓�
                mouseEvent: 'mouseover', //榧犳爣浜嬩欢
                tabedCallback: null //鍒囨崲鍚庣殑鍥炶皟鍑芥暟
            }, opt);

            if(typeof def.setup != Number && def.setup < 1) def.setup = 1;
            // 鍐呴儴閫氱敤鍙橀噺
            var self = def.content,
                content = self.find('[rel="xtaberItems"]'),
                subItem = content.find('.xtaber-item'),
                itemLength = subItem.length,
                subItemHeight = 
                    parseInt(subItem.height())+
                    parseInt(subItem.css('marginTop'))+
                    parseInt(subItem.css('marginBottom'))+
                    parseInt(subItem.css('paddingTop'))+
                    parseInt(subItem.css('paddingBottom')),
                subItemWidth = 
                    parseInt(subItem.width())+
                    parseInt(subItem.css('marginLeft'))+
                    parseInt(subItem.css('marginRight'))+
                    parseInt(subItem.css('paddingLeft'))+
                    parseInt(subItem.css('paddingRight')),
                scrollHeight = subItemHeight * def.setup,
                scrollWidth = subItemWidth * def.setup,
                screenNum,
                current = 0,
                autoTimer,
                itemTimer,
                tabItem = null;

            //婊氬姩灞忔暟                
            if(def.setup == 1){
                screenNum = itemLength;
            }
            else{
                var inAll = (itemLength % def.setup),
                    num = parseInt(itemLength / def.setup);
                screenNum = (inAll > 0) ? (num+1) : num;
            }                

            var init = function(){
                // 鑷姩鐢熸垚tab
                if(def.tab && typeof def.tab != 'object'){
                    var tabHTML = '<ol rel="xtaberTabs" class="xtaber-tabs">';
                    for(var i=1; i<=screenNum; i++){
                        tabHTML += '<li rel="xtaberTabItem">'+i+'</li>';
                    }
                    tabHTML += '</ol>';
                    self.append(tabHTML);
                    def.tab = self.find('[rel="xtaberTabs"]');
                }
                else if(typeof def.tab == 'object'){
                    def.tab = self.find('[rel="xtaberTabs"]');
                }
                else{
                    def.tab = null;
                }

                if(def.tab != null){
                    tabItem = def.tab.find('[rel="xtaberTabItem"]');
                }

                if(typeof def.next == 'boolean' && def.next){
                    def.next = $('<span rel="xtaberNext">next</span>');
                    def.next.appendTo(self);
                }
                if(typeof def.prev == 'boolean' && def.prev){
                    def.prev = $('<span rel="xtaberPrev">prev</span>');
                    def.prev.appendTo(self);
                }

                switch(def.style){
                    case 'left':
                        setParent('left');
                        break;
                    case 'top':
                        setParent('top');
                        break;
                }

                goTo(def.defaultShow - 1);
                
                bindEvent();
                if(def.auto){
                    auto();
                }

            }
            //璁剧疆鐖剁骇鐨勬牱寮�
            var setParent = function(type){
                var wrapHeight,wrapWidht,contentWidth,contentHeight;
                if(type == 'top'){
                    contentHeight = subItemHeight * itemLength;
                    contentWidth = subItemWidth;
                }
                else if(type == 'left'){
                    contentHeight = subItemHeight;
                    contentWidth = subItemWidth * itemLength;
                }
                //alert(typeof(subItemWidth));
                content.css({
                    left: 0,
                    top: 0,
                    position: 'absolute',
                    width: contentWidth,
                    height: contentHeight
                });
            }

            var goTo = function(index){
                clearInterval(autoTimer);
                clearTimeout(itemTimer);
                current = index;
                switch(def.style){
                    case 'top':
                        content.stop().animate({'top': -(index * scrollHeight)}, def.speed);
                        break;
                    case 'left':
                        content.stop().animate({'left': -(index * scrollWidth)}, def.speed);
                        break;
                    case 'opacity':
                        subItem.eq(index).fadeIn().siblings().hide();
                        break;
                    default:
                        subItem.eq(index).show().siblings().hide();
                        break;
                }
                if(def.tab != null){
                   tabItem.eq(index).addClass(def.activeClass).siblings().removeClass(def.activeClass);
                }
                if(def.auto){
                    auto()
                };
                if(def.tabedCallback){
                    tabedCallback();
                }
            }

            var auto = function(){
                if(def.auto){
                    clearInterval(autoTimer);
                    autoTimer = setInterval(function(){
                        if(current + 1 >= screenNum){
                                goTo(0);
                        }else{
                            goTo(current + 1);
                        }
                    }, def.timeout);
                }
            }
            //缁戝畾浜嬩欢
            var bindEvent = function(){
                if(def.tab != null){
                    tabItem.each(function(){
                        var el = $(this);
                        el.bind(def.mouseEvent, function(){
                            clearInterval(autoTimer);
                            clearTimeout(itemTimer);
                            itemTimer = setTimeout(function(){
                                goTo(el.index());
                            }, def.delay);
                        });
                        
                        el.bind('mouseleave', function(){
                            clearTimeout(itemTimer);
                            auto();
                        });
                    });
                }

                if(def.next){
                    def.next.click(function(){
                        var currentNum = (current + 1 >= screenNum) ? 0 : current + 1;
                        goTo(currentNum);  
                    });
                }
                
                if(def.prev){
                    def.prev.click(function(){
                        var currentNum = (current - 1 < 0) ? screenNum - 1 : current - 1;
                        goTo(currentNum);
                    });
                }
                
            }
            init();           
        }
    });
})(jQuery);

var isNeeded = function(selectors){
    var selectors = (typeof selectors == 'string') ? [selectors] : selectors,
        isNeeded;
    for(var i=0;i<selectors.length;i++){
        var selector = selectors[i];
        if( $(selector).length > 0 ) { 
            isNeeded = true; 
            break; 
        }
    };
    return isNeeded ;
};



$(function(){
    /* 棣栭〉鐒︾偣鍥� */
    if(isNeeded('#j_idx_focus')){
        var obj = $('#j_idx_focus');
        $.fn.xTaber({
            content: obj,
            tab: obj,
            auto: true,
            style: 'left',
            prev: obj.find('.btn-prev'),
            next: obj.find('.btn-next')
        });
    }
});