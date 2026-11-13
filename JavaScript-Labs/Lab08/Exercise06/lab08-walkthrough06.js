const stringOne = new String("Test");
const stringTwo = "Test";
const stringThree = "Test";
const stringFour = new String("Test");

document.write("<h2>String</h2><dl class='lab08-values'>");
document.write("<dt>stringOne</dt><dd>" + stringOne + "</dd>");
document.write("<dt>typeof stringOne</dt><dd>" + typeof stringOne + "</dd>");
document.write("<dt>typeof stringThree</dt><dd>" + typeof stringThree + "</dd>");
document.write("<dt>stringOne == stringTwo</dt><dd>" + (stringOne == stringTwo) + "</dd>");
document.write("<dt>stringOne == stringFour</dt><dd>" + (stringOne == stringFour) + "</dd>");
document.write("<dt>stringOne === stringTwo</dt><dd>" + (stringOne === stringTwo) + "</dd>");
document.write("<dt>stringTwo === stringThree</dt><dd>" + (stringTwo === stringThree) + "</dd>");
document.write("<dt>stringTwo === stringFour</dt><dd>" + (stringTwo === stringFour) + "</dd>");
document.write("</dl>");

const dateOne = new Date();
document.write("<h2>Date</h2><p>" + dateOne + "</p>");
document.write("<h2>Math</h2><dl class='lab08-values'>");
document.write("<dt>Math.PI</dt><dd>" + Math.PI + "</dd>");
document.write("<dt>Math.sqrt(4)</dt><dd>" + Math.sqrt(4) + "</dd>");
document.write("<dt>Math.random()</dt><dd>" + Math.random() + "</dd>");
document.write("</dl>");
