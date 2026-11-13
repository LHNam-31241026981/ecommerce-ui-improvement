// define constructor function for Country objects
function Country(name, iso, capital, population) {
    this.name = name;
    this.iso = iso;
    this.capital = capital;
    this.population = population;
}

// create an array of country objects
var countries = [
    new Country("Bahamas", "BS", "Nassau", 301790),
    new Country("Canada", "CA", "Ottawa", 33679000),
    new Country("Germany", "DE", "Berlin", 81802257),
];

// you can also push each new country object onto the end of the array
countries.push(new Country("Spain", "ES", "Madrid", 46505963));
countries.push(new Country("United Kingdom", "GB", "London", 62348447));

for (let index = 0; index < countries.length; index++) {
  const country = countries[index];

  document.write("<article class='lab08-tile country'>");
  document.write("<img src='flags/" + country.iso + ".png' alt='' width='48' height='48'>");
  document.write("<dl class='lab08-values'>");
  for (const propertyName in country) {
    if (Object.hasOwn(country, propertyName)) {
      document.write("<dt>" + propertyName + "</dt><dd>" + country[propertyName] + "</dd>");
    }
  }
  document.write("</dl></article>");
}
