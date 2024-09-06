<script lang="ts">
    import CustomCheckbox from "../../../../../../../components/shared/CustomCheckbox.svelte";

    export let isMultiple: boolean;
    export let minAnswersCount: number;
    export let maxAnswersCount: number;

    function showMultipleChoiceCssClass(): string {
        return isMultiple ? "show" : "hide";
    }
</script>

<div class="multi-choince-input-line">
    <div class="input-label is-multiple-input-label">
        Is multiple choice:
        <CustomCheckbox checked={isMultiple} />
    </div>
    <div class="multiple-choice-only-form-block {showMultipleChoiceCssClass()}">
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
