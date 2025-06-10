import { send } from "../utilities";
import { getUserId } from "./funcs";
import { Hotel } from "./types";

let query = new URLSearchParams(location.search);
let hotelId = parseInt(query.get("hotelId")!);
let userId = await getUserId();

let hotelH1 = document.querySelector("#hotelH1") as HTMLHeadingElement;
let ratingDiv = document.querySelector("#ratingDiv") as HTMLDivElement;
let ratingInput = document.querySelector("#ratingInput") as HTMLInputElement;
let ratingButton = document.querySelector("#ratingButton") as HTMLButtonElement;
let resInput = document.querySelector("#resInput") as HTMLInputElement;
let resButton = document.querySelector("#resButton") as HTMLButtonElement;
let hotelImg = document.querySelector("#hotelImg") as HTMLImageElement;
let resUl = document.querySelector("#resUl") as HTMLUListElement;
let mapIframe = document.querySelector("#mapIframe") as HTMLIFrameElement;


resInput.onchange = function () {
  if (userId != null && resInput.value != "") {
    resButton.disabled = false;
  }
};

resButton.onclick = async function () {
  let date = resInput.value;
  let success = await send("addReservation", [date, userId, hotelId]);

  if (success) {
    alert("Reservation created successfully!");
    location.reload();
  } else {
    alert("The chosen date is already reserved.");
  }
};

ratingInput.oninput = function () {
  if (ratingInput.value !== "") {
    let rating = parseFloat(ratingInput.value);
    if (rating < 0) ratingInput.value = "0";
    if (rating > 5) ratingInput.value = "5";
  }
};

ratingButton.onclick = async function () {
  let rating = parseFloat(ratingInput.value);
  if (!Number.isNaN(rating)) {
    await send("rate", [rating, userId, hotelId]);
    drawStars();
  } else {
    alert("Enter a valid rating.");
  }
};

async function drawStars() {
  let rating = await send("getAverage", hotelId) as number;

  ratingDiv.innerHTML = "";

  for (let i = 1; i <= 5; i++) {
    let img = document.createElement("img");
    img.classList.add("star");
    ratingDiv.appendChild(img);


    if (i <= rating) {
      img.src = "/website/images/full.star.png";
    } else if (i - 0.5 <= rating) {
      img.src = "/website/images/half.star.png";
    } else {
      img.src = "/website/images/empty.star.png";
    }
  }
}

let hotel = await send("getHotel", hotelId) as Hotel;
hotelH1.innerText = hotel.Name;
hotelImg.src = hotel.Image;

console.log(hotel.MapUrl);
mapIframe.src = hotel.MapUrl;


if (userId != null) {
  let ratingValue = await send("getRating", [userId, hotelId]) as string | null;
  if (ratingValue != null) {
    ratingInput.value = ratingValue;
  }
}

let dates = await send("getDates", hotelId) as string[];
for (let i = 0; i < dates.length; i++) {
  let date = dates[i];

  let li = document.createElement("li");
  li.innerText = date;
  resUl.appendChild(li);
}

drawStars();

if (userId != null) {
  ratingButton.disabled = false;
}
