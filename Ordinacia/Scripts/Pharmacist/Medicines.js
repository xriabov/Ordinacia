function deleteButton(elem) {
    const id = elem.parentNode.dataset.id;
    if (window.confirm("Are you sure?")) {
        window.location.replace("/Pharmacist/DeleteMedicine?id=" + id);
    }
}

function editButton(elem) {
    const id = elem.parentNode.dataset.id;
    const oldPrice = elem.parentNode.parentNode.parentNode.children[2].textContent.trim().slice(1);
    const newPrice = window.prompt("Enter new price:", oldPrice);
    if (!isNaN(parseFloat(newPrice))) {
        window.location.replace("/Pharmacist/EditMedicine?id=" + id + "&price=" + newPrice);
    }
}

function addButton() {
    const name = window.prompt("Enter name:");
    if (name !== "") {
        const price = window.prompt("Enter price:");
        if (!isNaN(parseFloat(price))) {
            window.location.replace("/Pharmacist/AddMedicine?name=" + name + "&price=" + price);
        }
    }
}