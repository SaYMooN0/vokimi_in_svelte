<script lang="ts">
    import {
        TestStylesArrowTypeUtils,
        type TestStylesArrowType,
    } from "../../../../ts/enums/TestStylesArrowType";

    export let prevBtnHidden: boolean;
    export let nextBtnHidden: boolean;
    export let backgroundColorAccent: string;
    export let btnArrowIcons: TestStylesArrowType;
    export let prevBtnClicked: () => void;
    export let nextBtnClicked: () => void;
</script>

<div class="control-btns">
    <button
        class:hide-btn={prevBtnHidden}
        class="prev-btn"
        style={backgroundColorAccent}
        on:click={prevBtnClicked}
    >
        <svelte:component
            this={TestStylesArrowTypeUtils.getIcon(btnArrowIcons)}
        />
        <span>Previous</span>
    </button>
    <button
        class:hide-btn={nextBtnHidden}
        class="next-btn"
        style={backgroundColorAccent}
        on:click={nextBtnClicked}
    >
        <span>Next</span>
        <svelte:component
            this={TestStylesArrowTypeUtils.getIcon(btnArrowIcons)}
        />
    </button>
</div>

<style>
    .control-btns {
        display: flex;
        justify-content: space-between;
        align-items: center;
        flex-direction: row;
        padding: 0 min(4vw, 40px);
    }
    .control-btns button {
        width: 160px;
        height: 40px;
        display: grid;
        align-items: center;
        gap: 4px;
        color: var(--back-main);
        border: none;
        border-radius: 8px;
        cursor: pointer;
        transition: transform 0.2s ease-in-out;
    }
    .control-btns button:hover {
        transform: scale(1.04);
    }
    .control-btns button:active {
        transform: scale(1.02);
    }
    .control-btns button span {
        font-size: 18px;
    }
    .hide-btn {
        transform: scale(0);
        pointer-events: none;
        opacity: 0;
    }
    .prev-btn {
        grid-template-columns: auto 1fr;
    }
    .next-btn {
        grid-template-columns: 1fr auto;
    }
    .control-btns button > :global(svg) {
        height: 80%;
        aspect-ratio: 1/1;
    }
    .next-btn > :global(svg) {
        transform: rotate(180deg);
    }
</style>
