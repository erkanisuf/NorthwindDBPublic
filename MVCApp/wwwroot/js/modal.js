let body = document.getElementById("modalBody");
let modalbtn = document.querySelectorAll(".modalbtn");

modalbtn.forEach(el => {
  el.addEventListener("click", e => {
    if (e.target.id) {
      getItems(e.target.id);
    } else {
      body.innerHTML = "<h3>error something went wrong !</h3>";
    }
  });
});
// Modal settins

//
function getItems(customerid) {
  body.innerHTML = "<h3>Loading data...</h3>";
  fetch(`Customers/Details/${customerid}`, { method: "GET" })
    .then(response => response.json())
    .then(data => {
      if (!data.orders.length) {
        body.innerHTML = "<h3>Customer has no orders in list!</h3>";
      } else {
        body.innerHTML = data.orders
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
          .join("");
      }
    });
}
