// File download helper
function downloadFile(filename, base64Data) {
    const link = document.createElement('a');
    link.href = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + base64Data;
    link.download = filename;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

// Auto-save indicator
window.showAutoSaveIndicator = function() {
    const indicator = document.getElementById('auto-save-indicator');
    if (indicator) {
     indicator.style.display = 'block';
        setTimeout(() => {
    indicator.style.display = 'none';
  }, 2000);
    }
};

// Keyboard shortcuts
document.addEventListener('DOMContentLoaded', function() {
    document.addEventListener('keydown', function(e) {
        // Ctrl+S to save
        if (e.ctrlKey && e.key === 's') {
            e.preventDefault();
            const saveButton = document.querySelector('[data-action="save"]');
            if (saveButton) saveButton.click();
        }
    
        // Ctrl+D to duplicate
        if (e.ctrlKey && e.key === 'd') {
          e.preventDefault();
            const duplicateButton = document.querySelector('[data-action="duplicate"]');
       if (duplicateButton) duplicateButton.click();
        }
 });
});
