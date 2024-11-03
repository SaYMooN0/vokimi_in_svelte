<script lang="ts">
    export let isChosen: boolean;
    export let isSingleChoice: boolean;
    export let accentColor: string = "var(--primary)";
</script>

<div
    class="is-chosen-indicator"
    class:is-chosen={isChosen}
    class:single-choice={isSingleChoice}
    class:multiple-choice={!isSingleChoice}
>
    <div></div>
</div>

<style>
    .is-chosen-indicator {
        box-sizing: border-box;
        padding: 4px;
        height: 24px;
        aspect-ratio: 1/1;
        background-color: var(--back-secondary);
        border: 2px solid transparent;
    }
    .is-chosen-indicator div {
        border-radius: inherit;
        height: 100%;
        width: 100%;
        background-color: transparent;
    }
    .is-chosen {
        border-color: var(--test-accent);
    }
    .is-chosen div {
        background-color: var(--test-accent) !important;
    }
    .single-choice {
        border-radius: 50%;
    }
    .multiple-choice {
        border-radius: 20%;
    }
</style>
