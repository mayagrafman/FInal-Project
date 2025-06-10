import { send } from "../utilities";
import { getUserId } from "./funcs";
import { Reservation } from "./types";

let reservationsDiv = document.querySelector("#reservationsDiv") as HTMLUListElement;

let userId = await getUserId();

let reservations = await send("getReservations", userId) as Reservation[];

for (let i = 0; i < reservations.length; i++) {
  let res = reservations[i];

  console.log(res);

  let div = document.createElement("div");
  div.classList.add("resDiv");
  reservationsDiv.appendChild(div);

  let img = document.createElement("img");
  img.src = res.Hotel.Image;
  img.width = 200;
  img.height = 120;
  div.appendChild(img);


let hotelCityDiv = document.createElement("div");
hotelCityDiv.innerText = "event hall: " + res.Hotel.Name;
let cityDiv = document.createElement("div");
cityDiv.innerText = "city: " + res.Hotel.City.Name;
div.appendChild(hotelCityDiv);
div.appendChild(cityDiv);


  let dateDiv = document.createElement("div");
  dateDiv.innerText = res.Date;
  div.appendChild(dateDiv);

  let unbookButton = document.createElement("button");
  unbookButton.innerText = "Unbook";
  unbookButton.onclick = function () {
    send("unbook", res.Id);
    div.remove();
  };
  div.appendChild(unbookButton);
}
