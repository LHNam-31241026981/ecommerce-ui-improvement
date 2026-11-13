const result = document.getElementById("console-result");

document.getElementById("show-logs").addEventListener("click", function () {
  console.log("script within the <head> element");
  console.warn("oh no this is a warning");
  console.error("panic!! error!!");
  result.hidden = false;
  result.textContent = "Đã in ba mức thông báo log, warn và error. Mở Console để xem.";
});

document.getElementById("show-typo").addEventListener("click", function () {
  try {
    doccumment.write("this text was written from JS");
  } catch (error) {
    console.error(error);
    result.hidden = false;
    result.textContent = error.name + ": " + error.message;
  }
});
