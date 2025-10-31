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
