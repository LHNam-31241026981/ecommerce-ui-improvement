$(function () {
    $("#apply-selectors").on("click", function () {
        const message = $("#msgArea");
        message.val("here is some new text");
        $("#first").html("new Title");
        message.val("My class is " + message.attr("class"));
        $("button").css("background-color", "red");
        $("body").css("background-color", "ivory");
        $(".center-icons").addClass("selected");
        $("#selector-status").text("Đã đổi tiêu đề, đọc lớp form-control, tô nút và thêm lớp selected.");
    });
});
