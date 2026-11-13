$(function () {
    const panel = $(".panel");
    const message = $("#message");

    panel.on("mousemove", function (event) {
        message.text(`x=${event.pageX}  y=${event.pageY}`);
    }).on("mouseleave", function () {
        message.text("goodbye!");
    }).on("click", function () {
        message.text("stopped move reporting");
        panel.off("mousemove");
    });
});
