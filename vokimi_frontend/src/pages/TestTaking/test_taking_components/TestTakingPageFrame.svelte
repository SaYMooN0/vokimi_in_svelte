<script lang="ts">
    import {
        TestStylesArrowTypeUtils,
        type TestStylesArrowType,
    } from "../../../ts/enums/TestStylesArrowType";

    export let accentColor: string;
    export let arrowsType: TestStylesArrowType;
    export let showPreviousButton: boolean;
    export let showNextButton: boolean;
</script>

<div class="test-taking-frame">
    <slot name="top-content" />
    <slot name="main-content" />
    <div class="control-btns">
        <button class="prev-btn">
            <svelte:component
                this={TestStylesArrowTypeUtils.getIcon(arrowsType)}
            />
            <label>Previous</label>
        </button>
        <button class="next-btn">
            <svelte:component
                this={TestStylesArrowTypeUtils.getIcon(arrowsType)}
            />
            <label>Next</label>
        </button>
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
