let currentModal;

async function loadModal(entity, action, id = "") {
    const modalId = `${action.toLowerCase()}-${entity.toLowerCase()}-modal`;
    const container = document.getElementById("modal-placeholder");

    const url = id ? `/${entity}/${action}/${id}` : `/${entity}/${action}`;

    try {
        const response = await fetch(url);
        if (!response.ok) throw new Error(`Failed to load ${action} modal for ${entity}`);

        const html = await response.text();
        container.innerHTML = html;

        currentModal = new bootstrap.Modal(document.getElementById(modalId));
        currentModal.show();
    } catch (error) {
        console.error("Modal Load Error:", error);
        alert(`Something went wrong while loading the ${action} form.`);
    }
}

async function submitModalForm(form, event, statusCode) {
    event.preventDefault();

    try {
        const response = await fetch(form.action, {
            method: "POST",
            body: new FormData(form),
        });

        if (!response.ok) {
            window.location.href = "/Home/Error";
            return;
        }

        if (response.status !== statusCode) {
            const errorHtml = await response.text();
            const errorDiv = document.createElement("div");
            errorDiv.innerHTML = errorHtml;

            const newContent = errorDiv.querySelector(".modal-content").innerHTML;
            form.closest(".modal-content").innerHTML = newContent;

            return;
        }

        currentModal.hide();
        window.location.reload();
    } catch (error) {
        console.error("Submission error:", error);
    }
}
