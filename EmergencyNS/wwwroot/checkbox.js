window.initializeCheckboxes = () => {
    const selectAllCheckbox = document.getElementById("select-all");
    const rowCheckboxes = document.querySelectorAll(".row-checkbox");

    selectAllCheckbox.addEventListener("change", () => {
        rowCheckboxes.forEach(checkbox => {
            checkbox.checked = selectAllCheckbox.checked;
        });
    });

    rowCheckboxes.forEach(checkbox => {
        checkbox.addEventListener("change", () => {
            selectAllCheckbox.checked = Array.from(rowCheckboxes).every(checkbox => checkbox.checked);
        });
    });
};


