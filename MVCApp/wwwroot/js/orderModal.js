let body = document.getElementById("orderModalBody");
let modalbtn = document.querySelectorAll(".modalbtn");
let span = document.getElementById("orderID");
let aspBtn = document.getElementById("asporderID");
let btnDetailDel = document.querySelectorAll(".delDetail");
modalbtn.forEach(el => {
  el.addEventListener("click", e => {
    if (e.target.id) {
      span.innerHTML = e.target.id;
      aspBtn.setAttribute("href", "Orders/OrderDetailCreate/" + e.target.id);
      console.log(aspBtn.getAttribute("asp-route-id"));
      getItems(e.target.id);
    } else {
      body.innerHTML = "<h3>error !</h3>";
    }
  });
});
// Modal settins

//
function getItems(orderId) {
  body.innerHTML = "<h3>Loading data...</h3>";
  fetch(`Orders/Details/${orderId}`, { method: "GET" })
    .then(response => response.json())
    .then(data => {
      if (!data) {
        body.innerHTML = "<h3>no products in order!</h3>";
      } else {
        body.innerHTML = data
          .map(
            el =>
              `

<div class="card rounded p-2 m-2" >
    <div class="d-flex flex-column bd-highlight mb-3">
 <div class="p-2 bd-highlight border-bottom ">

<div class="d-flex justify-content-center">
<button class="btn-warning rounded m-2" data-bs-toggle="tooltip"  data-bs-placement="top" title="Edit"><a  href="Orders/OrderDetailEdit/${"order" +
                el.orderId +
                "product" +
                el.productId}" ><i class="bi bi-pen"> </a></i>
            <button id="${el.orderId}" class="btn-danger delDetail rounded m-2"
                onclick="deleteItem(${el.product.productId})"
                data-bs-toggle="tooltip"
                data-bs-placement="top" 
                 title="Delete">
                <i class="bi bi-trash2-fill" id="${
                  el.orderId
                }"    onclick="deleteItem(${el.product.productId})">
                </i></button>
</div>

                </div>
          <img height="160px"  style="object-fit:contain;" src="${
            el.product.imageLink
              ? el.product.imageLink
              : "https://st3.depositphotos.com/23594922/31822/v/600/depositphotos_318221368-stock-illustration-missing-picture-page-for-website.jpg"
          }" class="card-img-top" alt="${data.productName}">
             <div class="p-2 bd-highlight border-bottom"> Product name:  ${
               el.product.productName
             }</div>
             <div class="p-2 bd-highlight border-bottom">Unit Price:  ${
               el.unitPrice
             }</div>
             <div class="p-2 bd-highlight border-bottom"> Quantity:  ${
               el.quantity
             }</div>
             <div class="p-2 bd-highlight border-bottom"> Discount:   ${
               el.discount
             }</div>
             <div class="p-2 bd-highlight border-bottom"> OrderID:  ${
               el.orderId
             }</div>
           


             
</div>    
</div>

`
          )
          .join("");
      }

      deleteItem();
    })
    .catch(er => console.log(er));
}

function deleteItem(item) {
  btnDetailDel = document.querySelectorAll(".delDetail");
  btnDetailDel.forEach(el => {
    el.addEventListener("click", e => {
      if (e.target.id && item) {
        console.log("orderid", Number(e.target.id));
        console.log("productid", Number(item));
        let data = {
          bodyproductID: Number(item),
          bodyorderID: Number(e.target.id)
        };
        fetchDeleteProduct(data);
      }
    });
  });
}

function fetchDeleteProduct(data) {
  body.innerHTML = "<h3>Loading!</h3>";
  fetch(`Orders/DelDetail`, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "X-ANTI-FORGERY-TOKEN": document.getElementsByName(
        "__RequestVerificationToken"
      )[0].value,

      "Content-Type": "application/json",
      Pragma: "no-cache"
    },
    body: JSON.stringify(data)
  })
    .then(response => response.json())
    .then(data => getItems(data.bodyorderID))
    .catch(er => console.log(er));
}
