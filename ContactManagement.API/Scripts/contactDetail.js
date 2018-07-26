function DeleteConfirmation() {
    if (confirm("Are you sure you want to delete this contact??")) {
        return true;
    }
    else {
        return false;
    }
}