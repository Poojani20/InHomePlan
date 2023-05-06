const prevBtns = document.querySelectorAll(".btn-prev");
const nextBtns = document.querySelectorAll(".btn-next");
const progress = document.getElementById("progress");
const formSteps = document.querySelectorAll(".form-step");
const progressSteps = document.querySelectorAll(".progress-step");

let formStepsNum = 0;

nextBtns.forEach((btn) => {
    btn.addEventListener("click", (e) => {
        e.preventDefault(); // Prevent the default form submission

        if (validateForm()) { // Validate the form before proceeding
            formStepsNum++;
            updateFormSteps();
            updateProgressbar();
        }
    });
});

//nextBtns.forEach((btn) => {
//    btn.addEventListener("click", (e) => {
//        e.preventDefault(); // Prevent the default form submission

//        const currentFormStep = formSteps[formStepsNum];
//        const formInputs = currentFormStep.querySelectorAll("input, select, textarea");

//        let isValid = true;
//        formInputs.forEach((input) => {
//            if (!input.checkValidity()) {
//                isValid = false;
//                input.classList.add("is-invalid"); // Add "is-invalid" class to inputs with invalid values

//                const errorContainer = input.closest(".form-group").querySelector(".text-danger");
//                errorContainer.textContent = input.validationMessage; // Display validation message
//            } else {
//                input.classList.remove("is-invalid"); // Remove "is-invalid" class from valid inputs

//                const errorContainer = input.closest(".form-group").querySelector(".text-danger");
//                errorContainer.textContent = ""; // Clear any previous validation message
//            }
//        });

//        if (isValid) {
//            formStepsNum++;
//            updateFormSteps();
//            updateProgressbar();
//        }
//    });
//});


prevBtns.forEach((btn) => {
    btn.addEventListener("click", () => {
        formStepsNum--;
        updateFormSteps();
        updateProgressbar();
    });
});

function updateFormSteps() {
    formSteps.forEach((formStep) => {
        formStep.classList.contains("form-step-active") &&
            formStep.classList.remove("form-step-active");
    });

    formSteps[formStepsNum].classList.add("form-step-active");
}

function updateProgressbar() {
    progressSteps.forEach((progressStep, idx) => {
        if (idx < formStepsNum + 1) {
            progressStep.classList.add("progress-step-active");
        } else {
            progressStep.classList.remove("progress-step-active");
        }
    });

    const progressActive = document.querySelectorAll(".progress-step-active");

    progress.style.width =
        ((progressActive.length - 1) / (progressSteps.length - 1)) * 100 + "%";
}

function validateForm() {
    // Get the current form step
    const currentFormStep = formSteps[formStepsNum];

    // Get the form inputs within the current form step
    const formInputs = currentFormStep.querySelectorAll("input, select, textarea");

    // Check if any of the inputs is invalid
    let isValid = true;
    formInputs.forEach((input) => {
        if (!input.checkValidity()) {
            input.reportValidity();
            isValid = false;
        }
    });

    return isValid;
}

//video
//var dataTable;

//$(document).ready(function () {
//    loadDataTable();
//});

//function loadDataTable() {
//    dataTable = $('#tblData').DataTable({
//        "ajax": { url: '/Application/getall' },
//        "columns": [
//            { data: 'id', "width": "25%" },
//            { data: 'firstname', "width": "15%" },
//            { data: 'lastname', "width": "15%" },
//            { data: 'address', "width": "15%" },
//            { data: 'phonenumber', "width": "10%" },
//            { data: 'nic', "width": "10%" },
//            { data: 'email', "width": "10%" },
//            { data: 'dateofbirth', "width": "10%" },
//            { data: 'status', "width": "10%" },

//            {
//                data: 'id',
//                "render": function (data) {
//                    return '<div class="w-75 btn-group">'
//                },
//                "width":"25%"
//            }
//        ]
//    })
//}
