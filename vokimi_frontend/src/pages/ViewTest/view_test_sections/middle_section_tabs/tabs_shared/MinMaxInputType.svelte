<script lang="ts">
    export let minPossibleValue: number | null;
    export let maxPossibleValue: number | null;
    export let minVal: number | null;
    export let maxVal: number | null;
    export let isFloat: boolean = false;
    $: stepValue = isFloat ? 0.01 : 1;
</script>

<div class="min-max-input-type">
    From
    <input
        type="number"
        bind:value={minVal}
        min={minPossibleValue}
        max={maxPossibleValue}
        step={stepValue}
    />
    To
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
        gap: 4px;
    }
    .min-max-input-type input {
        width: 80px;
    }
</style>
