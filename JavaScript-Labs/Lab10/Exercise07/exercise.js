$(function () {
    const photos = $(".lab10-travel img");
    const controls = $(".lab10-travel button");
    let pointer;

    function removePreview() {
        photos.removeClass("gray");
        $("#preview").remove();
    }

    function movePreview(event) {
        pointer = event;
        const preview = $("#preview");
        // Giữ ảnh xem trước trong khung nhìn khi con trỏ sát mép phải.
        const left = Math.min(event.clientX + 30, window.innerWidth - preview.outerWidth() - 8);
        const top = Math.min(event.clientY - 10, window.innerHeight - preview.outerHeight() - 8);
        preview.css({ left: Math.max(8, left), top: Math.max(8, top) });
    }

    function showPreview(photo, event) {
        removePreview();
        const caption = photo.attr("alt");
        const source = photo.attr("src").replace("/small/", "/medium/");
        const preview = $("<div>", { id: "preview" }).hide();
        preview.append($("<img>", { src: source, alt: caption }), $("<p>").text(caption));
        $("body").append(preview);
        photo.addClass("gray");

        if (event) {
            movePreview(event);
            preview.find("img").one("load", function () {
                if (preview[0].isConnected) {
                    movePreview(pointer);
                }
            });
        } else {
            preview.addClass("lab10-preview-fixed");
        }

        preview.fadeIn(1000);
    }

    photos.on("mouseover", function (event) {
        if (window.matchMedia("(hover: hover)").matches) {
            showPreview($(this), event);
        }
    }).on("mousemove", movePreview).on("mouseleave", removePreview);

    controls.on("focus", function () {
        if (this.matches(":focus-visible")) {
            showPreview($(this).find("img"));
        }
    }).on("blur", removePreview).on("click", function () {
        if (window.matchMedia("(hover: none)").matches) {
            const photo = $(this).find("img");
            if (photo.hasClass("gray")) {
                removePreview();
            } else {
                showPreview(photo);
            }
        }
    });

    $(document).on("keydown", function (event) {
        if (event.key === "Escape") {
            removePreview();
        }
    });
});
