<script lang="ts">
    import {
        TestStylesArrowType,
        TestStylesArrowTypeUtils,
    } from "../../../../../ts/enums/TestStylesArrowType";

    export let chosenType: TestStylesArrowType = TestStylesArrowType.Default;

    function chooseIconType(type: TestStylesArrowType) {
        chosenType = type;
    }
</script>

<div class="arrow-picker">
    <div class="type-choosing-container">
        {#each Object.values(TestStylesArrowType) as arrowType}
            <div
                class="arrow-type-card"
                class:chosen={arrowType === chosenType}
                on:click={() => chooseIconType(arrowType)}
            >
                <div class="arrows-container">
                    <svelte:component
                        this={TestStylesArrowTypeUtils.getIcon(arrowType)}
                    />
                    <svelte:component
                        this={TestStylesArrowTypeUtils.getIcon(arrowType)}
                    />
                </div>
                <div class="chosen-indicator">
                    <span
                        class:chosen-indicator-circle={arrowType === chosenType}
                    >
                    </span>
                </div>
            </div>
        {/each}
    </div>
</div>

<style>
    .type-choosing-container {
        width: fit-content;
        justify-self: center;
        align-self: center;
        display: flex;
        flex-direction: row;
        justify-content: space-evenly;
        gap: 28px;
        margin-bottom: 6px;
    }
    .arrow-type-card {
        display: grid;
        grid-template-rows: 1fr auto;
        gap: 4px;
        padding: 4px 12px;
        border-radius: 8px;
        border: 3px solid transparent;
        box-shadow: 0px 1px 6px 3px rgba(0, 0, 0, 0.1);
        cursor: pointer;
    }
    .arrows-container {
        display: grid;
        grid-template-columns: 1fr 1fr;
        width: 100%;
        gap: 12px;
    }
    .arrows-container :global(svg:nth-child(2)) {
        transform: rotate(180deg);
    }
    .arrows-container :global(svg) {
        height: 36px;
        aspect-ratio: 1/1;
        color: var(--text);
    }
    .chosen {
        border-color: var(--primary);
        box-shadow: 0px 1px 8px 4px rgb(47, 43, 56, 0.1);
    }

    .chosen-indicator {
        justify-self: center;
        width: fit-content;
        height: 20px;
        aspect-ratio: 1/1;
        padding: 2px;
        box-sizing: border-box;
        border: 2px solid var(--text-faded);
        border-radius: 15px;
    }

    .chosen-indicator-circle {
        display: block;
        height: 100%;
        width: 100%;
        background-color: var(--primary);
        border-radius: 10px;
    }
</style>
