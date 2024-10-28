<script lang="ts">
    import {
        TestStylesArrowTypeUtils,
        type TestStylesArrowType,
    } from "../../../ts/enums/TestStylesArrowType";

    export let accentColor: string;
    export let arrowsType: TestStylesArrowType;
    export let showPreviousButton: boolean;
    export let prevButtonOnclickAction: () => void;
    export let showNextButton: boolean;
    export let nextButtonOnclickAction: () => void;
</script>

<div class="test-taking-frame">
    <slot name="top-content" />
    <slot name="main-content" />
    <div class="control-btns">
        {#if showPreviousButton}
            <button
                class="prev-btn"
                style="background-color: {accentColor};"
                on:click={prevButtonOnclickAction}
            >
                <svelte:component
                    this={TestStylesArrowTypeUtils.getIcon(arrowsType)}
                />
                <span>Previous</span>
            </button>
        {/if}
        {#if showNextButton}
            <button
                class="next-btn"
                style="background-color: {accentColor};"
                on:click={nextButtonOnclickAction}
            >
                <svelte:component
                    this={TestStylesArrowTypeUtils.getIcon(arrowsType)}
                />
                <span>Next</span>
            </button>
        {/if}
    </div>
</div>

<style>
    .control-btns {
        display: flex;
        justify-content: space-between;
        align-items: center;
        flex-direction: row;
    }
    .control-btns button {
        width: 240px;
        height: 48px;
        display: grid;
        color: var(--back-main);
    }
    .prev-btn {
        grid-template-columns: auto 1fr;
    }
    .next-btn {
        grid-template-columns: 1fr auto;
    }
    .control-btns button > :global(svg) {
        height: 100%;
        aspect-ratio: 1/1;
    }
    .next-btn > :global(svg) {
        transform: rotate(180deg);
    }
</style>
