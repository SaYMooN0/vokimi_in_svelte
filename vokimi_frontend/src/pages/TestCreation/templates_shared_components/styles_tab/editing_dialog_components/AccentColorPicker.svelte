<script lang="ts">
    export let accentColor: string;
    let isCustomChosen = false;

    const defaultColors = ["#ff6c1f", "#e46b67", "#6d88f4", "#f05593"];

    function isDefaultColorChosen(color: string) {
        return !isCustomChosen && color === accentColor;
    }

    function chooseColor(newColor: string) {
        accentColor = newColor;
        isCustomChosen = !defaultColors.includes(newColor);
    }

    function onCustomColorChanged(event: { target: { value: string } }) {
        chooseColor(event.target.value);
    }
</script>

<div class="color-picker">
    <div class="colors-container">
        {#each defaultColors as color}
            <div
                class="color-card {isDefaultColorChosen(color) ? 'chosen' : ''}"
                on:click={() => {
                    isCustomChosen = false;
                    chooseColor(color);
                }}
                style="background-color:{color};"
            >
                <label class="color-label">{color}</label>
            </div>
        {/each}

        <div
            class="color-card {isCustomChosen ? 'chosen' : ''}"
            on:click={() => {
                isCustomChosen = true;
            }}
        >
            <div id="custom-color-container">
                <input type="color" bind:value={accentColor} />
                <label for="customColor">My Color</label>
            </div>
        </div>
    </div>
</div>

<style>
    .colors-container {
        display: flex;
        flex-direction: row;
        gap: 20px;
    }

    #custom-color-container {
        display: flex;
        flex-direction: column;
    }

    #customColor {
        appearance: none;
        -moz-appearance: none;
        -webkit-appearance: none;
        background: none;
        border: 0;
        padding: 0;
        height: 100px;
        width: 100px;
        cursor: pointer;
    }

    #customColor:focus {
        border-radius: 0;
        outline: none;
    }

    ::-webkit-color-swatch-wrapper {
        padding: 0;
    }

    ::-webkit-color-swatch {
        border: 0;
        border-radius: 6px;
    }

    ::-moz-color-swatch,
    ::-moz-focus-inner {
        border: 0;
        border-radius: 6px;
    }

    ::-moz-focus-inner {
        padding: 0;
        border-radius: 6px;
    }
    .color-card {
        height: 200px;
        width: 120px;
        padding: 20px 16px 4px 16px;
        border: 4px solid transparent;
        display: grid;
        grid-template-rows: 1fr auto auto;
        justify-content: center;
        box-shadow: 0px 1px 8px 4px rgba(0, 0, 0, 0.1);
        border-radius: 10px;
    }

    .chosen {
        border-color: var(--primary);
        box-shadow: 0px 1px 8px 4px rgb(47, 43, 56, 0.1);
    }

    .color-label {
        text-align: center;
        color: black;
        font-weight: bold;
        font-size: 20px;
        letter-spacing: 1px;
    }

    .is-chosen-indicator {
        margin-top: 6px;
        justify-self: center;
        width: fit-content;
        height: 20px;
        aspect-ratio: 2.6/1;
        padding: 2px;
        box-sizing: border-box;
        border: 2px solid var(--back-2);
        border-radius: 15px;
    }

    .is-chosen-indicator span {
        display: block;
        height: 100%;
        width: 100%;
        background-color: var(--primary);
        border-radius: 10px;
    }
</style>
