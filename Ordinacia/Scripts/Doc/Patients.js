
document.onkeydown = ev => {
    console.log(ev);
    if(ev.key === "Escape")
        window.location.replace("/Doctor/Patients");
};

renderMedicines();
function renderMedicines(){
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState === 4 && this.status === 200) {
            document.querySelector("#med-data").innerHTML = this.responseText;
        }
    };
    let val = document.querySelector("#CurrentPharmacy").value;
    let patient = document.querySelector("#patient").dataset.id;
    if(!patient)
        patient = 0;
    xhttp.open("GET", "/Doctor/RenderMedicines?currentPharmacy="+val+"&currentPatient="+patient, true);
    xhttp.send();
}

function viewButton(elem) {
    console.log(elem);
    var id = elem.parentNode.dataset.patientId;
    var oldId = document.querySelector("#patient").dataset.id;
    document.querySelectorAll(".btn-group").forEach(function(a) {
        if(a.dataset.patientId === oldId) {
            var oldElem = a;
            oldElem.childNodes[3].classList.remove("btn-primary");
            oldElem.childNodes[3].classList.add("btn-outline-primary");
        }
    });
    // #patient data-id to id
    document.querySelector("#patient").dataset.id = id;
    elem.classList.remove("btn-outline-primary");
    elem.classList.add("btn-primary");
    // request patient data and medicines
    var xhttp = new XMLHttpRequest();

    xhttp.open("GET", "/Doctor/GetPatientData?id=" + id, false);
    xhttp.send();
    document.querySelector("#patient").innerHTML = xhttp.responseText;
    xhttp.abort();

    xhttp.open("GET", "/Doctor/GetPatientMedicines?id=" + id, false);
    xhttp.send();
    document.querySelector("#med-patient").innerHTML = xhttp.responseText;
}

function deleteMed(elem){
    const xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Doctor/DeleteMedicine?patient="+elem.dataset.pat+"&medicine="+elem.dataset.med, false);
    xhttp.send();
    
    document.querySelectorAll(".btn-group").forEach(function(a) {
        if(a.dataset.patientId === elem.dataset.pat) {
            console.log(a.childNodes[3]);
            viewButton(a.childNodes[3]);
        }
    });
}

function addMed(elem){
    const xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Doctor/AddMedicine?medicineId="+elem.dataset.med+"&currentPatient="+elem.dataset.pat, false);
    xhttp.send();
    
    document.querySelectorAll(".btn-group").forEach(function(a) {
        if(a.dataset.patientId === elem.dataset.pat) {
            console.log(a.childNodes[3]);
            viewButton(a.childNodes[3]);
        }
    });
}

function deletePatient(elem){
    if(!window.confirm("Are you sure?"))
        return;
    const xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Doctor/DeletePatient?patient="+elem.parentNode.dataset.patientId);
    xhttp.send();
    window.location.replace("/Doctor/Patients");
}

function addPatient(){
    var elem = document.querySelector("#patient");
    
    const xhttp = new XMLHttpRequest();
    xhttp.open("GET", "/Doctor/AddPatient/", false);
    xhttp.send();
    elem.innerHTML = xhttp.responseText;
    console.log(xhttp.responseText);
}

