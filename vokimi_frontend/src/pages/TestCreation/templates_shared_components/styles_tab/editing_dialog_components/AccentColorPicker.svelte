<script lang="ts">
    export let accentColor: string;

    const defaultColors = [
        "#796cfa",
        "#ff6c1f",
        "#e46b67",
        "#6d88f4",
        "#f05593",
    ];
    let isCustomChosen: boolean = !defaultColors.includes(accentColor);
    function chooseColor(newColor: string) {
        accentColor = newColor;
    }
    $: {
        isCustomChosen = !defaultColors.includes(accentColor);
    }
    let lastCustom = !defaultColors.includes(accentColor)
        ? accentColor
        : "#7197b7";

    if (isCustomChosen) {
        lastCustom = accentColor;
    }
</script>

<div class="color-picker">
    <div class="colors-container">
        {#each defaultColors as color}
            <div
                class="color-card"
                class:chosen={!isCustomChosen && color === accentColor}
                on:click={() => {
                    isCustomChosen = false;
                    chooseColor(color);
                }}
            >
                <div class="color-div" style="background-color:{color};"></div>
                <p class="color-label">{color}</p>
            </div>
        {/each}

        <div
            class="color-card"
            class:chosen={isCustomChosen}
            on:click={() => {
                isCustomChosen = true;
                accentColor = lastCustom;
            }}
        >
            <div>
                <input
                    id="customColor"
                    type="color"
                    on:change={() => (accentColor = lastCustom)}
                    bind:value={lastCustom}
                />
                <label for="customColor">My Color</label>
            </div>
            <p class="color-label">{lastCustom}</p>
        </div>
    </div>
</div>

<style>
    .colors-container {
        display: flex;
        flex-direction: row;
        gap: 20px;
        --color-div-border-rad: 6px;
        --color-div-size: 100px;
        padding: 4px 20px;
    }

    .color-card {
        height: 168px;
        width: 120px;
        padding: 20px 16px 4px 16px;
        border: 4px solid transparent;
        display: flex;
        flex-direction: column;
        align-items: center;
        box-shadow: 0px 1px 8px 4px rgba(0, 0, 0, 0.1);
        border-radius: 10px;
        cursor: pointer;
        transition: transform 0.08s ease-in;
    }
    .color-card:hover {
        transform: scale(1.04);
    }

    .chosen {
        border-color: var(--primary);
    }
    .color-div {
        border: 0;
        padding: 0;
        height: var(--color-div-size);
        width: var(--color-div-size);
        border-radius: var(--color-div-border-rad);
    }
    .color-label {
        text-align: center;
        color: black;
        font-weight: bold;
        font-size: 20px;
        margin-top: auto;
        margin-bottom: 12px;
    }
    #customColor {
        display: block;
        appearance: none;
        -moz-appearance: none;
        -webkit-appearance: none;
        background: none;
        border: 0;
        padding: 0;
        height: var(--color-div-size);
        width: var(--color-div-size);
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
        border-radius: var(--color-div-border-rad);
    }

    ::-moz-color-swatch,
    ::-moz-focus-inner {
        border: 0;
        border-radius: var(--color-div-border-rad);
    }

    ::-moz-focus-inner {
        padding: 0;
        border-radius: 6px;
    }
</style>
