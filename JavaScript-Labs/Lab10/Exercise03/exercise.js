$(function () {
    const form = $("form");
    const inputs = form.find("input");
    const labels = form.find("label");
    const position = $("form :nth-child(4n)");
    const password = $(":password");
    const matchingLabels = $('label:contains("word")');

    $("#position-selector").on("click", function () {
        inputs.css("background-color", "");
        labels.css("color", "");
        position.css("background-color", "yellow");
        matchingLabels.css("color", "black");
        $("#selector-status").text(`Đã tô ${position.length} trường theo vị trí và đổi màu ${matchingLabels.length} nhãn chứa "word".`);
    });

    $("#password-selector").on("click", function () {
        inputs.css("background-color", "");
        labels.css("color", "");
        password.css("background-color", "yellow");
        $("#selector-status").text(`Đã tô ${password.length} trường mật khẩu.`);
    });

    form.on("submit", function (event) {
        event.preventDefault();
        if ($("#pass1").val() !== $("#pass2").val()) {
            $("#selector-status").text("Hai trường mật khẩu phải trùng nhau.");
            return;
        }
        $("#selector-status").text("Biểu mẫu hợp lệ. Dữ liệu được giữ trong trình duyệt.");
    });

    $("#position-selector").trigger("click");
});
