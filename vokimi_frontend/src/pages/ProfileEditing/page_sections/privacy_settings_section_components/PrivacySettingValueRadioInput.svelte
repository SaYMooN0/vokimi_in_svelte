<script lang="ts">
    import {
        PrivacyValues,
        PrivacyValuesUtils,
    } from "../../../../ts/enums/PrivacyValues";
    import { StringUtils } from "../../../../ts/utils/StringUtils";

    export let value: PrivacyValues;
    let idPrefix: string = StringUtils.randomString(8);
</script>

<div class="privacy-inputs">
    {#each Object.values(PrivacyValues) as privVal}
        <label class="privacy-radio unselectable">
            <input
                type="radio"
                name="privacy-radio-{idPrefix}"
                checked={value === privVal}
            />
            <span class="privacy-radio-span">
                {PrivacyValuesUtils.getFullName(privVal)}
            </span>
        </label>
    {/each}
</div>

<style>
    .privacy-inputs {
        position: relative;
        display: flex;
        flex-wrap: wrap;
        border-radius: 8px;
        background-color: var(--back-secondary);
        box-sizing: border-box;
        padding: 4px;
        font-size: 14px;
        gap: 4px;
    }

    .privacy-radio {
        flex: 1 1 auto;
        text-align: center;
    }

    .privacy-radio input {
        display: none;
    }

    .privacy-radio-span {
        display: flex;
        cursor: pointer;
        align-items: center;
        justify-content: center;
        border-radius: 6px;
        border: none;
        padding: 8px 12px;
        color: var(--text-faded);
        transition: all 0.15s ease-in;
    }

    .privacy-radio input:checked + .privacy-radio-span {
        background-color: var(--primary);

        color: var(--back-main);
    }
</style>
