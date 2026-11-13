$(function () {
    const load = $("#consume");
    const list = $("#results ul");
    const status = $("#request-status");

    load.on("click", function () {
        load.prop("disabled", true);
        list.empty();
        status.text("Đang gửi GET…");

        $.get("../data/cities.json", function (cities, textStatus) {
            $.each(cities, function (index, city) {
                $("<li>").text(city.name).appendTo(list);
            });

            status.text(`GET ${textStatus}: đã tải ${cities.length} thành phố.`);
        }, "json").fail(function (xhr, textStatus) {
            status.text(`GET chưa trả về danh sách (${textStatus}, HTTP ${xhr.status}).`);
        }).always(function () {
            load.prop("disabled", false);
        });
    });
});
