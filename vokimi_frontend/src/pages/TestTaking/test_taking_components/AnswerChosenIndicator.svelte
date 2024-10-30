<script lang="ts">
    export let isChosen: boolean;
    export let isSingleChoice: boolean;
</script>

<div
    class="is-chosen-indicator"
    class:is-chosen={isChosen}
    class:single-choice={isSingleChoice}
    class:multiple-choice={!isSingleChoice}
>
    <span></span>
</div>

<style>
    .is-chosen-indicator {
        padding: 2px;
        height: 32px;
        aspect-ratio: 1/1;
        background-color: var(--back-secondary);
    }
    .is-chosen-indicator span {
        border-radius: inherit;
        height: 100%;
        width: 100%;
        background-color: transparent;
    }
    .is-chosen span {
        background-color: var(--accent);
    }
    .single-choice {
        border-radius: 50%;
    }
    .multiple-choice {
        border-radius: 2%;
    }
</style>
