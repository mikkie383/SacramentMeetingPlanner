// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.querySelector(".calling2").setAttribute("style", "display:none;");
document.querySelector(".calling3").setAttribute("style", "display:none;");
var count = 2;
function addCalling() {

    document.querySelector(".calling" + count).setAttribute("style", "display:block;");
    count++;
    /*if (count <= 3) {
        let div = document.createElement("div");
        let label = document.createElement("label");
        let select = document.createElement("select");
        let option = document.createElement("option");

        div.setAttribute("class", "form-group");
        label.textContent = "Calling";
        label.setAttribute("class", "control-label");
        select.setAttribute("class", "form-control");
        select.setAttribute("asp-items", "@ViewBag.Calling");
        option.setAttribute("value", "");
        option.textContent = "-- Select a Calling --";
        select.appendChild(option);
        div.appendChild(label);
        div.appendChild(select);
        document.querySelector(".calling").appendChild(div);
        count++;
    }*/
}

function checkLimit() {
    let check = document.getElementsByName("selectedCallings");
    let newvar = 0;
    for (let i = 0; i < check.length; i++) {
        if (check[i].checked == true) {
            newvar += 1;
        }
    }
    let limit = 4;
    if (newvar >= limit) {
        alert("You can only select a maximum of " + (limit - 1 ) + " callings");
        return false;
    }
}

