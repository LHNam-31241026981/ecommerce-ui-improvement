// boxClass có phạm vi toàn cục trong classic script.
var boxClass = "movingDiv";

function outputBox(num) {
  var box = "<div class='" + boxClass + "' id='div" + num + "'>";
  box += "This is div " + num;
  box += "</div>";
  return box;
}
