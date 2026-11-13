$(function () {
    const figure = $("figure");
    const status = $("#effect-status");
    const effects = {
        btnFadeIn: ["fadeIn", 1000],
        btnFadeOut: ["fadeOut", 1000],
        btnFade: ["fadeToggle", 500],
        btnSlideDown: ["slideDown", "slow"],
        btnSlideUp: ["slideUp", "slow"],
        btnSlide: ["slideToggle", 2000]
    };

    $("#commandLine").on("click", "button", function () {
        const [method, duration] = effects[this.id];
        figure.stop(true, true)[method](duration, function () {
            const state = figure.is(":visible") ? "Ảnh đang hiện." : "Ảnh đã ẩn.";
            status.text(`${method}(${duration}) đã chạy. ${state}`);
        });
    });
});
