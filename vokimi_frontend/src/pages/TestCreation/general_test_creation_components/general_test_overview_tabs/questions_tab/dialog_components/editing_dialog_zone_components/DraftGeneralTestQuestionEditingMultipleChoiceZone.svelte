<script lang="ts">
    import CustomCheckbox from "../../../../../../../components/shared/CustomCheckbox.svelte";

    export let isMultiple: boolean;
    export let minAnswersCount: number;
    export let maxAnswersCount: number;

    function showMultipleChoiceCssClass(): string {
        return "";
    }
</script>

<div class="multi-choince-input-line">
    <div class="input-label is-multiple-input-label">
        Is multiple choice:
        <CustomCheckbox isChecked={isMultiple} />
    </div>
    <div class="multiple-choice-only-form-block {isMultiple ? 'show' : 'hide'}">
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
</div>

<style>
    .multiple-choice-only-form-block {
        width: 100%;
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: space-evenly;
    }
    .show {
        animation: slideIn 0.5s forwards;
    }

    .hide {
        display: none !important;
    }
</style>
