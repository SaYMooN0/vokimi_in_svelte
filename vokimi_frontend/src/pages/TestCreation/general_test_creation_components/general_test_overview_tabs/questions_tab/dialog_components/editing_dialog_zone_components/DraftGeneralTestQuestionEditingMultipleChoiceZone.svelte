<script lang="ts">
    import CustomCheckbox from "../../../../../../../components/shared/CustomCheckbox.svelte";

    export let isMultiple: boolean;
    export let minAnswersCount: number;
    export let maxAnswersCount: number;
</script>

<div class="input-label">
    Is multiple choice:
    <CustomCheckbox bind:isChecked={isMultiple} />
</div>

<div
    class="multiple-choice-only-form-block"
    class:show={isMultiple}
    class:hide={!isMultiple}
>
    <label for="min-answers-count" class="input-label">
        Minimum answers count:
        <input
            id="min-answers-count"
            type="number"
            bind:value={minAnswersCount}
            min="1"
        />
    </label>

    <label for="max-answers-count" class="input-label">
        Maximum answers count:
        <input
            id="max-answers-count"
            type="number"
            bind:value={maxAnswersCount}
        />
    </label>
</div>

<style>
    .multiple-choice-only-form-block {
        width: 100%;
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: space-evenly;
    }
    .input-label input[type="number"] {
        width: 62px;
        box-sizing: border-box;
        background-color: var(--back-secondary);
        outline: none;
        height: 32px;
        border: 2px solid transparent;
        border-radius: 4px;
        padding: 0 6px;
        font-size: 20px;
        text-align: center;
    }

    .input-label input[type="number"]:focus {
        border-color: var(--primary);
    }
    .show {
        animation: slideIn 0.5s forwards;
        display: flex;
    }

    .hide {
        display: none !important;
    }

    @keyframes slideIn {
        from {
            opacity: 0;
            transform: translateY(-20px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
</style>
