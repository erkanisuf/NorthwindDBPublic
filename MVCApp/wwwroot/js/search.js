const select = document.getElementById("items");
let input, filter, table, tr, td, i, txtValue;
let selectedFilter = 1;
function onSelect() {
  selectedFilter = Number(select.value);
}

function filterTable() {
  // Declare variables

  input = document.getElementById("myInput");
  filter = input.value.toUpperCase();
  table = document.getElementById("myTable");
  tr = table.getElementsByTagName("tr");

  // if match filter show else dont
  for (i = 0; i < tr.length; i++) {
    td = tr[i].getElementsByTagName("td")[selectedFilter];
    if (td) {
      txtValue = td.textContent || td.innerText;
      if (txtValue.toUpperCase().indexOf(filter) > -1) {
        tr[i].style.display = "";
      } else {
        tr[i].style.display = "none";
      }
    }
  }
}
