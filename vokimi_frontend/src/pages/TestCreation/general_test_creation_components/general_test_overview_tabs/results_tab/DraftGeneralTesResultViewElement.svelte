<script lang="ts">
    import type { DraftGeneralTestResultDataToView } from "../../../../../ts/test_creation_tabs_classes/general_test_creation/results/GeneralTestCreationResultsTabData";

    export let result: DraftGeneralTestResultDataToView;

    let isHidden: boolean = true;
</script>

<div class="result-view">
    <div class="always-shown">
        <label class="result-name">
            Result Name: {result.name}
        </label>

        <svg
            xmlns="http://www.w3.org/2000/svg"
            xmlns:xlink="http://www.w3.org/1999/xlink"
            viewBox="0 0 24 24"
            fill="none"
            class="content-toogle-btn"
            class:rotated={isHidden}
            on:click={() => (isHidden = !isHidden)}
        >
            <path
                d="M18 15C18 15 13.5811 9.00001 12 9C10.4188 8.99999 6 15 6 15"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </div>
    <div class:hiddenContent={isHidden} class:resultInnerContent={!isHidden}>
        <!--    
        text
        image
        removeBtn -->
    </div>
</div>
