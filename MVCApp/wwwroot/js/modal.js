let body = document.getElementById("modalBody");
let modalbtn = document.querySelectorAll(".modalbtn");

modalbtn.forEach(el => {
  el.addEventListener("click", e => {
    if (e.target.id) {
      getItems(e.target.id);
    }
  });
});
// Modal settins
var myModal = document.getElementById("modalid");
var myInput = document.getElementById("myInput");

if (myModal) {
  myModal.addEventListener("shown.bs.modal", function(e) {
    myInput.focus();
  });
}

//
function getItems(customerid) {
  fetch(`Customers/Details/${customerid}`, { method: "GET" })
    .then(response => response.json())
    .then(
      data =>
        (body.innerHTML = data.orders
          .map(
            el => `
  <tr>
<td>${el.orderDate}</td>
<td>${el.requiredDate}</td>
<td>${el.shipAddress}</td>
<td>${el.shipCity}</td>
<td>${el.shipCountry}</td>
<td>${el.shipName}</td>
<td>${el.shipPostalCode}</td>
<td>${el.freight}</td>
</tr>

`
          )
          .join(""))
    );
}
