function addvote(staffid, classid) {
    if (classid == 170) {
        alert("该项目投票已结束，谢谢您的参与！")
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Vote/AddVoteCount/",
        data: {
            "staffid": staffid
        },
        success: function (data) {
            console.log(data);

            if (data.ReturnCode == 0)
            {
                if (data.Data == true)
                {
                    var count = $("#btn_" + staffid).html();
               
                    $("#btn_" + staffid).html(parseInt(count) + 1);
                }

                alert(data.Message);
            } 
        },
        error: function (error) {
            console.log(error.responseText);

            alert("应用程序错误，请联系系统管理员！");
        }
    });
}