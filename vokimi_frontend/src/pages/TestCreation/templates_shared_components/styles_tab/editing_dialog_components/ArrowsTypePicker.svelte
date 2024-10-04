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
                <span class="arrow-name">
                    {TestStylesArrowTypeUtils.getFullName(arrowType)}
                </span>
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
        gap: 30px;
        margin-bottom: 6px;
    }
    .arrow-type-card {
        display: flex;
        flex-direction: column;
        padding: 8px 12px;
        gap: 8px;
        box-shadow: 0px 1px 6px 3px rgba(0, 0, 0, 0.1);
        border-radius: 8px;
        border: 3px solid transparent;
        cursor: pointer;
    }
    .arrows-container {
        display: grid;
        grid-template-columns: 1fr 1fr;
        width: 100%;
    }
    .arrows-container :global(svg:nth-child(2)) {
        transform: rotate(180deg);
    }
    .arrows-container :global(svg) {
        height: 24px;
    }
    .chosen {
        border-color: var(--primary);
        box-shadow: 0px 1px 8px 4px rgb(47, 43, 56, 0.1);
    }
</style>
