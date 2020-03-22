const data = [
  {
    id:1,
    firstname: "Martin",
    lastname: "Müller",
    streetAndNumber: "Hauptstraße 56",
    zipCode: "44894",
    city: "Bochum",
    lat: 0,
    long: 0,
    ok:false
  },
  {
    id:3,
    firstname: "Christian",
    lastname: "Lockert",
    streetAndNumber: "Maximilianstraße 66",
    zipCode: "85051",
    city: "Ingolstadt",
    lat: 0,
    long: 0,
    ok:false
  },
  {
    id:4,
    firstname: "<firstname>",
    lastname: "<lastname>",
    streetAndNumber: "Martin-Ludwig-Straße 17",
    zipCode: "85080",
    city: "Gaimersheim",
    lat: 0,
    long: 0,
    ok:false
  },
  {
    id:5,
    firstname: "Hana",
    lastname: "Maxim",
    streetAndNumber: "Ulrichstraße 23",
    zipCode: "73734",
    city: "Esslingen am Neckar",
    lat: 0,
    long: 0,
    ok:false
  }
];
export default data;

export const backendUrl = 'https://c19test.azurewebsites.net/api'
// export const backendUrl = 'https://localhost:5001/api'