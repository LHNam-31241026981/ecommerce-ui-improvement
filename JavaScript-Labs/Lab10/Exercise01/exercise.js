function showJQuery(source) {
    const $ = window.jQuery;
    $("#jquery-status").text(`jQuery ${$.fn.jquery} đã tải từ ${source}.`);
    $("#check-jquery").prop("disabled", false).on("click", function () {
        $("#jquery-demo").text("jQuery đã chọn phần tử này và cập nhật nội dung bằng .text().");
    });
}

if (window.jQuery) {
    showJQuery("CDN");
} else {
    const script = document.createElement("script");
    script.src = "../vendor/jquery-3.7.1.min.js";
    script.addEventListener("load", function () {
        showJQuery("tệp cục bộ");
    });
    script.addEventListener("error", function () {
        document.getElementById("jquery-status").textContent = "Không tải được jQuery.";
    });
    document.head.append(script);
}
