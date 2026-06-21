window.gameRenderer = {
    draw: function (canvasId, data) {
        const canvas = document.getElementById(canvasId);
        if (!canvas) return;
        const ctx = canvas.getContext('2d');
        const theme = data.theme || {
            backgroundColor: '#1a1a1a',
            snakeColor: '#4caf50',
            snakeHeadColor: '#81c784',
            foodColor: '#f44336',
            textColor: 'white',
            fontFamily: 'Arial'
        };

        // Clear canvas
        ctx.fillStyle = theme.backgroundColor;
        ctx.fillRect(0, 0, data.width, data.height);

        // Draw grid if specified
        if (theme.gridColor) {
            ctx.strokeStyle = theme.gridColor;
            ctx.lineWidth = 0.5;
            for (let x = 0; x <= data.width; x += data.cellSize) {
                ctx.beginPath();
                ctx.moveTo(x, 0);
                ctx.lineTo(x, data.height);
                ctx.stroke();
            }
            for (let y = 0; y <= data.height; y += data.cellSize) {
                ctx.beginPath();
                ctx.moveTo(0, y);
                ctx.lineTo(data.width, y);
                ctx.stroke();
            }
        }

        // Draw snake
        for (let i = 0; i < data.snake.length; i++) {
            const part = data.snake[i];
            ctx.fillStyle = (i === 0) ? theme.snakeHeadColor : theme.snakeColor;
            ctx.fillRect(part.x * data.cellSize, part.y * data.cellSize, data.cellSize - 1, data.cellSize - 1);
        }

        // Draw food
        ctx.fillStyle = theme.foodColor;
        ctx.fillRect(data.food.x * data.cellSize, data.food.y * data.cellSize, data.cellSize - 1, data.cellSize - 1);

        // Game Over overlay
        if (data.isGameOver) {
            ctx.fillStyle = 'rgba(0, 0, 0, 0.7)';
            ctx.fillRect(0, 0, data.width, data.height);

            ctx.fillStyle = theme.textColor || 'white';
            ctx.font = `30px ${theme.fontFamily || 'Arial'}`;
            ctx.textAlign = 'center';
            ctx.fillText('GAME OVER', data.width / 2, data.height / 2);
            ctx.font = `20px ${theme.fontFamily || 'Arial'}`;
            ctx.fillText('Score: ' + data.score, data.width / 2, data.height / 2 + 40);
            ctx.fillText('Press Space to Restart', data.width / 2, data.height / 2 + 80);
        }
    }
};
