// Chart.js helper functions for Blazor interop
window.chartHelper = {
    charts: {},
    createChart: function (canvasId, type, data, options) {
        // Destroy existing chart if it exists
        if (this.charts[canvasId]) {
            this.charts[canvasId].destroy();
        }
        const ctx = document.getElementById(canvasId);
        if (!ctx) {
            console.error('Canvas element not found:', canvasId);
            return;
        }
        this.charts[canvasId] = new Chart(ctx, {
            type: type,
            data: data,
            options: options
        });
    },
    updateChart: function (canvasId, data) {
        if (this.charts[canvasId]) {
            this.charts[canvasId].data = data;
            this.charts[canvasId].update();
        }
    },
    destroyChart: function (canvasId) {
        if (this.charts[canvasId]) {
            this.charts[canvasId].destroy();
            delete this.charts[canvasId];
        }
    }
};

// Helper to download a file from base64 payload
window.downloadFile = (fileName, base64) => {
    try {
        // Convert base64 to blob for better browser compatibility
        const byteCharacters = atob(base64);
        const byteNumbers = new Array(byteCharacters.length);

        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);

        // Determine MIME type based on file extension
        let mimeType = 'application/octet-stream';
        if (fileName.endsWith('.csv')) {
            mimeType = 'text/csv';
        } else if (fileName.endsWith('.xlsx')) {
            mimeType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
        } else if (fileName.endsWith('.xls')) {
            mimeType = 'application/vnd.ms-excel';
        } else if (fileName.endsWith('.pdf')) {
            mimeType = 'application/pdf';
        } else if (fileName.endsWith('.txt')) {
            mimeType = 'text/plain';
        }

        const blob = new Blob([byteArray], { type: mimeType });

        // Create download link
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = fileName;

        // Trigger download
        document.body.appendChild(link);
        link.click();

        // Cleanup
        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);

        console.log(`File "${fileName}" downloaded successfully`);
    }
    catch (err) {
        console.error('downloadFile error:', err);
        alert('Error downloading file: ' + err.message);
    }
};