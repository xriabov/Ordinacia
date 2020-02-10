function deleteButton(elem) {
    const id = elem.parentNode.dataset.id;
    if (window.confirm("Are you sure?")) {
        window.location.replace("/Pharmacist/DeleteMedicine?id=" + id);
    }
}

function editButton(elem) {
    const id = elem.parentNode.dataset.id;
    const newPrice = window.prompt("Enter new price:");
    if (!isNaN(newPrice)) {
        window.location.replace("/Pharmacist/EditMedicine?id=" + id + "&price=" + newPrice);
    }
}

function addButton() {
    const name = window.prompt("Enter name:");
    if (name !== "") {
        const price = window.prompt("Enter price:");
        if (!isNaN(price)) {
            window.location.replace("/Pharmacist/AddMedicine?name=" + name + "&price=" + price);
        }
    }
}