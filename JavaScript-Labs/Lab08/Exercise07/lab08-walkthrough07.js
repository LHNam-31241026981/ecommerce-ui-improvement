const daysOfWeek = new Array("Mon", "Tues", "Wed", "Thur", "Fri");
daysOfWeek.push("Sat");
daysOfWeek.unshift("Sun");

document.write("<table class='lab08-calendar'><thead><tr>");
for (let index = 0; index < daysOfWeek.length; index++) {
  let day = daysOfWeek[index];
  if (day.length >= 4) {
    day = "<em>" + day + "</em>";
  }
  document.write("<th scope='col'>" + day + "</th>");
}
document.write("</tr></thead><tbody>");

const daysInMonth = 30;
for (let startOfWeek = 1; startOfWeek <= daysInMonth; startOfWeek += daysOfWeek.length) {
  document.write("<tr>");
  for (let column = 0; column < daysOfWeek.length; column++) {
    const date = startOfWeek + column;
    document.write("<td>" + (date <= daysInMonth ? date : "") + "</td>");
  }
  document.write("</tr>");
}
document.write("</tbody></table>");
