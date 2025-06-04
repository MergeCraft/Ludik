// src/lib/fontawesome.js
import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faQrcode,
  faXmark,
  faRightToBracket,
  faUserPlus,
  faArrowRightFromBracket,
  faMagnifyingGlassArrowRight,
  faBars,
  faUser,
  faUsers,
  faCogs,
  faFilter,
  faAngleDown,
  faCamera,
  faFileUpload,
} from "@fortawesome/free-solid-svg-icons";
// Si usás marcas:
// import { faFacebook, faTwitter } from '@fortawesome/free-brands-svg-icons';

library.add(faCamera, faFileUpload, faQrcode, faAngleDown, faFilter, faXmark, faRightToBracket, faUserPlus, faArrowRightFromBracket, faMagnifyingGlassArrowRight, faBars, faUser, faUsers, faCogs); // Agrega a la librería
