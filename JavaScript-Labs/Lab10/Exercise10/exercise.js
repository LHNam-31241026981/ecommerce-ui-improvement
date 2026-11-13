$(function () {
    const buttons = $("#consume, #consume-invalid");
    const loading = $(".animLoading");
    const results = $("#results");
    const list = $("#results ul");
    const status = $("#request-status");

    function requestCities(url) {
        buttons.prop("disabled", true);
        status.text("Đang gửi GET…");
        list.empty();
        results.attr("aria-busy", "true");
        loading.prop("hidden", false).show();

        const request = $.get(url, null, null, "json");

        request.done(function (cities, textStatus, xhr) {
            $.each(cities, function (index, city) {
                $("<li>").text(city.name).appendTo(list);
            });

            status.text(`done(): HTTP ${xhr.status}, ${textStatus}. Đã tải ${cities.length} thành phố.`);
        }).fail(function (xhr, textStatus, error) {
            const reason = textStatus === "parsererror" ? "Phản hồi không phải JSON hợp lệ." : String(error);
            status.text(`fail(): ${textStatus}, HTTP ${xhr.status}. ${reason}`);
        }).always(function () {
            loading.fadeOut("slow", function () {
                loading.prop("hidden", true);
                results.attr("aria-busy", "false");
                buttons.prop("disabled", false);
                status.append(" always(): đã tắt chỉ báo tải.");
            });
        });
    }

    $("#consume").on("click", function () {
        requestCities("../data/cities.json");
    });

    $("#consume-invalid").on("click", function () {
        requestCities("../data/html-response.html");
    });
});
