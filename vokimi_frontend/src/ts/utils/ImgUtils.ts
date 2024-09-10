import { StringUtils } from "./StringUtils";

export namespace ImgUtils {
    export function saveImgApiUrl(fileKey: string): string {
        return `/api/vokimisaveimg/${fileKey}`;
    }
    export function imgUrl(fileKey: string): string {
        return `/api/vokimiimgs/${fileKey}`;
    }
    export function imgUrlWithVersion(path: string): string {
        return `${imgUrl(path)}?v=${StringUtils.randomString(10)}`;
    }
}
