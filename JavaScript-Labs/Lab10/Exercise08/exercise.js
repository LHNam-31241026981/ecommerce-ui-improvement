$(function () {
    const box = $("#box");
    const stage = $(".lab10-stage");
    const animate = $("#animate");
    const status = $("#animation-status");

    $("#reset").on("click", function () {
        box.stop(true).removeAttr("style");
        animate.prop("disabled", false);
        status.text("Sẵn sàng chạy ba bước.");
    });

    animate.on("click", function () {
        animate.prop("disabled", true);
        box.removeAttr("style");
        const right = Math.min(495, stage.innerWidth() - box.outerWidth() - 12);
        status.text("Bước 1: đi ngang, tăng độ đậm và thu thấp.");

        box.animate({ left: right, opacity: 1, height: 50 }, function () {
            status.text("Bước 2: đi xuống và giảm độ đậm.");
        }).animate({ top: 400, opacity: 0.2 }, function () {
            status.text("Bước 3: tăng chiều cao lên 200 px.");
        }).animate({ height: 200 }, 2000, function () {
            status.text("Hoàn tất 3 bước animate() theo thứ tự.");
            animate.prop("disabled", false);
        });
    });
});
