function onDeleteClicked(elem) {
    const id = elem.parentNode.dataset.id;
    if (!window.confirm("Are you sure?")) return;
    const xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Doctor/DeleteCoWorker?id=" + id, false);
    xhttp.send();
    window.location.replace("/Doctor/CoWorkers");
}

function onEditClicked(elem) {
    var password = window.prompt("Enter new password");
    if (password !== "") {
        const xhttp = new XMLHttpRequest();
        xhttp.open("GET", "/Doctor/EditCoWorker?id=" + elem.parentNode.dataset.id + "password=" + password, false);
        xhttp.send();
        window.location.replace("/Doctor/CoWorkers");
    }
}