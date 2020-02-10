function renderMedicines(elem)
{
    const id = elem.dataset.id;

    const xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/InsuranceWorker/RenderMedicines?id="+id, false);
    xhttp.send();
    document.querySelector("#medicines").innerHTML = xhttp.responseText;
}