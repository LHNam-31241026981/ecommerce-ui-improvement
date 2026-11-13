$(function () {
    const panel = $(".panel");
    const image = $("<img>", {
        src: "../assets/13030.jpg",
        alt: "Ảnh thu nhỏ tác phẩm nghệ thuật từ bộ tệp Lab 10",
        class: "inserted-image"
    });
    const methods = {
        after: function () { panel.after(image); },
        before: function () { panel.before(image); },
        append: function () { panel.append(image); },
        prepend: function () { panel.prepend(image); },
        appendTo: function () { image.appendTo(panel); },
        prependTo: function () { image.prependTo(panel); },
        insertBefore: function () { image.insertBefore(panel); }
    };

    $("#positions").on("click", "button", function () {
        const method = $(this).data("method");
        methods[method]();
        $("#positions button").attr("aria-pressed", "false");
        $(this).attr("aria-pressed", "true");
        $("#insertion-status").text(`Vừa chạy ${method}().`);
    });

    $('[data-method="insertBefore"]').trigger("click");
});
