const fpsInterval = 1000 / 30;

let frameCounter = 0;

function loop() {
    frameCounter++;

    if (frameCounter % 5 === 0) {
        methodEvery5Frames();
    }

    if (frameCounter % 30 === 0) {
        methodEvery30Frames();
        frameCounter = 0; // Reset the frame counter every 30 frames to avoid overflow
    }
}

function methodEvery30Frames() {
    console.log('Method called every 30 frames');
}

function methodEvery5Frames() {
    console.log('Method called every 5 frames');
}

// Start the loop using setInterval
const intervalId = setInterval(loop, fpsInterval);
