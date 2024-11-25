<script lang="ts">
    export let minPossibleValue: number | null;
    export let maxPossibleValue: number | null;
    export let minVal: number | null;
    export let maxVal: number | null;
    export let isFloat: boolean = false;
    $: stepValue = isFloat ? 0.01 : 1;
</script>

<div class="min-max-input-type">
    <span>From</span>
    <input
        type="number"
        bind:value={minVal}
        min={minPossibleValue}
        max={maxPossibleValue}
        step={stepValue}
    />
    <span>To</span>
    <input
        type="number"
        bind:value={maxVal}
        min={minPossibleValue}
        max={maxPossibleValue}
        step={stepValue}
    />
</div>

<style>
    .min-max-input-type {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 6px;
    }
    .min-max-input-type span {
        margin-left: 12px;
        color: var(--text-faded);
        font-size: 18px;
    }
    .min-max-input-type input {
        width: 64px;
        padding: 0 4px;
        outline: none;
        border: 2px solid var(--text-faded);
        border-radius: 4px;
        background-color: var(--back-main);
        font-size: 18px;
        text-align: center;
    }
    .min-max-input-type input:focus {
        border-color: var(--primary);
    }
</style>
