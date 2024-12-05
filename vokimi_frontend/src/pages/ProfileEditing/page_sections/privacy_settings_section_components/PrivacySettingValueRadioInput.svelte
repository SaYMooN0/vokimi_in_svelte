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
        <label class="privacy-radio">
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
        border-radius: 0.5rem;
        background-color: #eee;
        box-sizing: border-box;
        box-shadow: 0 0 0px 1px rgba(0, 0, 0, 0.06);
        padding: 0.25rem;
        width: 300px;
        font-size: 14px;
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
        border-radius: 0.5rem;
        border: none;
        padding: 0.5rem 0;
        color: var(--text-faded);
        transition: all 0.15s ease-in-out;
    }

    .privacy-radio input:checked + .privacy-radio-span {
        background-color: var(--primary);
        font-weight: 600;
    }
</style>
